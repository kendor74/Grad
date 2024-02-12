const APP_ID = "9dad5dba660e43fbba0421f35d54aa9d";

let uid = sessionStorage.getItem("uid");

if (!uid) {
    uid = String(Math.floor(Math.random() * 10000));
    sessionStorage.setItem("uid", uid);
}

let token = null;
let client;

const querryString = window.location.search;
const urlParameter = new URLSearchParams(querryString);

let rommId = urlParameter.get("room");

let userName = `user-${uid}`;

if (!rommId) {
    rommId = "main";
}

let localTracks = [];
let remorteUsers = {};

let localScreenTracks;
let sharingScreen = false;

let joinRoomInit = async () => {
    client = AgoraRTC.createClient({ mode: "rtc", codec: "vp8" });
    await client.join(APP_ID, rommId, token, uid);

    console.log(`user${uid} joined`);

    client.on("user-published", handelUserPublished);
    client.on("user-left", handelUserLeft);
    joinStream();
};

let joinStream = async () => {
    localTracks = await AgoraRTC.createMicrophoneAndCameraTracks(
        {},
        {
            encoderConfig: {
                width: { min: 640, ideal: 1920, max: 1920 },
                height: { min: 480, ideal: 1080, max: 1080 },
            },
        }
    );

    let player = `<div class="video-player host-img" id="user-${uid}"></div>`;
    document.getElementById("col-1").insertAdjacentHTML("afterbegin", player);
    //document.getElementById(`user-${uid}`).addEventListener('click',expandVideoFrame)
    localTracks[1].play(`user-${uid}`);
    await client.publish([localTracks[0], localTracks[1]]);
};

let switchToCamera = async () => {
    let player = `<div class="video-player host-img" id="user-${uid}"></div>`;
    document.getElementById("col-1").insertAdjacentHTML("afterbegin", player);

    await localTracks[0].setMuted(false);
    await localTracks[1].setMuted(false);

    document.getElementById("screen-btn").classList.remove("active");
    document.getElementById("mic-btn").classList.remove("active");

    localTracks[1].play(`user-${uid}`);
    await client.publish([localTracks[1]]);
};

let handelUserPublished = async (user, mediaType) => {
    remorteUsers[user.uid] = user;

    await client.subscribe(user, mediaType);

    let player = document.getElementById(`user-${user.uid}`);
    if (player === null) {
        player = `<div class="video-player remote-img" id="user-${user.uid}"></div>`;

        document
            .getElementById("remote-users")
            .insertAdjacentHTML("beforeend", player);
        //document.getElementById(`user-${user.uid}`).addEventListener('click',expandVideoFrame)
    }

    if (mediaType === "video") {
        user.videoTrack.play(`user-${user.uid}`);
    }
    if (mediaType === "audio") {
        user.audioTrack.play();
    }
};

let handelUserLeft = async (user) => {
    delete remorteUsers[user.uid];
    document.getElementById(`user-${user.uid}`).remove();

    if (userIdInDisplayFrame === `user-${user.uid}`) {
        displayFrame.style.display = null;

        let videoFrames = document.getElementsByClassName("remote-users");

        for (let i = 0; videoFrames.length > i; i++) {
            videoFrames[i].style.height = "300px";
            videoFrames[i].style.width = "300px";
        }
    }
};

let toggelMic = async (e) => {
    let button = e.currentTarget;

    if (!localTracks[0].muted) {
        button.classList.add("active");
        await localTracks[0].setMuted(true);

    } else {
        button.classList.remove("active");
        await localTracks[0].setMuted(false);

    }
};


//putting logo on mutting
let toggelCamera = async (e) => {
    let button = e.currentTarget;
    let parent = document.getElementById(`user-${uid}`)

    if (localTracks[1].muted) {
        button.classList.remove("camera-active");
        parent.classList.remove("hosting");
        await client.unpublish([localTracks[1]]);

        await localTracks[1].setMuted(false);
    } else {
        await localTracks[1].setMuted(true);
        parent.classList.add("hosting");
        button.classList.add("camera-active");
    }
};

let toggelScreen = async (e) => {
    let screenButton = e.currentTarget;
    let cameraButton = document.getElementById("camera-btn");
    let displayFrame = document.getElementById("col-1");
    if (!sharingScreen) {
        sharingScreen = true;

        screenButton.classList.add("screen-active");
        cameraButton.classList.remove("camera-active");
        cameraButton.style.display = "none";

        localScreenTracks = await AgoraRTC.createScreenVideoTrack();

        document.getElementById(`user-${uid}`).remove();
        displayFrame.style.display = "block";

        let player = `<div class="video-player host-img" id="user-${uid}"></div>`;

        displayFrame.insertAdjacentHTML("afterbegin", player);
        //document.getElementById(`user-container-${uid}`).addEventListener('click', expandVideoFrame)

        userIdInDisplayFrame = `user-${uid}`;

        localScreenTracks.play(`user-${uid}`);

        await client.unpublish([localTracks[1]]);
        await client.publish([localScreenTracks]);
    } else {
        sharingScreen = false;
        cameraButton.style.display = "block";
        screenButton.classList.remove("screen-active");

        document.getElementById(`user-${uid}`).remove(); // remove the html element
        await client.unpublish([localScreenTracks]); // remove the screen sharing from senign to other nodes

        switchToCamera();
    }
};

//fixing the expanding video frame
// let expandVideoFrame = (e) => {

//   let child = displayFrame.children[0]
//   if(child){
//       document.getElementById('col-1').appendChild(child)
//   }

//   displayFrame.style.display = 'block'
//   displayFrame.appendChild(e.currentTarget)
//   userIdInDisplayFrame = e.currentTarget.id

//   for(let i = 0; videoFrames.length > i; i++){
//     if(videoFrames[i].id != userIdInDisplayFrame){
//       videoFrames[i].style.height = '100px'
//       videoFrames[i].style.width = '100px'
//     }
//   }

// }



document.getElementById("camera-btn").addEventListener("click", toggelCamera);
document.getElementById("mic-btn").addEventListener("click", toggelMic);
document.getElementById("screen-btn").addEventListener("click", toggelScreen);

joinRoomInit();
