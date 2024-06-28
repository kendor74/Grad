const APP_ID = "9dad5dba660e43fbba0421f35d54aa9d";

let uid = sessionStorage.getItem("uid");

if (!uid) {
    uid = String(Math.floor(Math.random() * 10000));
    sessionStorage.setItem("uid", uid);
}

let token = null;
let client;

const queryString = window.location.search;
const urlParameter = new URLSearchParams(queryString);

let roomId = urlParameter.get("room");

let userName = `user-${uid}`;

if (!roomId) {
    roomId = "main";
}

let localTracks = [];
let remoteUsers = {};

let localScreenTracks;
let sharingScreen = false;

let joinRoomInit = async () => {
    client = AgoraRTC.createClient({ mode: "rtc", codec: "vp8" });
    await client.join(APP_ID, roomId, token, uid);

    console.log(`user${uid} joined`);

    client.on("user-published", handleUserPublished);
    client.on("user-left", handleUserLeft);
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
    document.getElementById("local_user").insertAdjacentHTML("beforeend", player);
    document.getElementById(`user-${uid}`).addEventListener('click', () => expandVideoFrame(`user-${uid}`));
    localTracks[1].play(`user-${uid}`);
    await client.publish([localTracks[0], localTracks[1]]);
};

let switchToCamera = async () => {
    let player = `<div class="video-player host-img" id="user-${uid}"></div>`;
    document.getElementById("local_user").insertAdjacentHTML("beforeend", player);

    await localTracks[0].setMuted(false);
    await localTracks[1].setMuted(false);

    document.getElementById("screen-btn").classList.remove("active");
    document.getElementById("mic-btn").classList.remove("active");

    localTracks[1].play(`user-${uid}`);
    await client.publish([localTracks[1]]);
};

let handleUserPublished = async (user, mediaType) => {
    remoteUsers[user.uid] = user;

    await client.subscribe(user, mediaType);

    let player = document.getElementById(`user-${user.uid}`);
    if (player === null) {
        player = `<div class="video-player remote-img" id="user-${user.uid}"></div>`;

        document.getElementById("remote-users").insertAdjacentHTML("beforeend", player);
        document.getElementById(`user-${user.uid}`).addEventListener('click', () => expandVideoFrame(`user-${user.uid}`));
    }

    if (mediaType === "video") {
        user.videoTrack.play(`user-${user.uid}`);
    }
    if (mediaType === "audio") {
        user.audioTrack.play();
    }
};

let handleUserLeft = async (user) => {
    delete remoteUsers[user.uid];
    document.getElementById(`user-${user.uid}`).remove();
};

let toggleMic = async (e) => {
    let button = e.currentTarget;

    if (!localTracks[0].muted) {
        button.classList.add("active");
        await localTracks[0].setMuted(true);
    } else {
        button.classList.remove("active");
        await localTracks[0].setMuted(false);
    }
};

let toggleCamera = async (e) => {
    let button = e.currentTarget;

    if (localTracks[1].muted) {
        button.classList.remove("camera-active");
        await localTracks[1].setMuted(false);
    } else {
        await localTracks[1].setMuted(true);
        button.classList.add("camera-active");
    }
};

let toggleScreen = async (e) => {
    let screenButton = e.currentTarget;
    let cameraButton = document.getElementById("camera-btn");
    let localUser = document.getElementById("local_user");
    if (!sharingScreen) {
        sharingScreen = true;

        localUser.style.display = "none";
        screenButton.classList.add("screen-active");
        cameraButton.classList.remove("camera-active");
        cameraButton.style.display = "none";

        localScreenTracks = await AgoraRTC.createScreenVideoTrack();

        document.getElementById(`user-${uid}`).remove();

        let player = `<div class="video-player host-img" id="user-${uid}"></div>`;
        document.getElementById("local_user").insertAdjacentHTML("afterbegin", player);
        document.getElementById(`user-${uid}`).addEventListener('click', () => expandVideoFrame(`user-${uid}`));

        localScreenTracks.play(`user-${uid}`);

        await client.unpublish([localTracks[1]]);
        await client.publish([localScreenTracks]);
    } else {
        sharingScreen = false;
        localUser.style.display = "block";
        cameraButton.style.display = "block";
        screenButton.classList.remove("screen-active");

        document.getElementById(`user-${uid}`).remove();
        await client.unpublish([localScreenTracks]);

        switchToCamera();
    }
};

function expandVideoFrame(streamId) {
    const localUserContainer = document.getElementById('local_user');
    const remoteUsersContainer = document.getElementById('remote-users');
    const selectedStream = document.getElementById(streamId);

    if (!selectedStream || !remoteUsersContainer || !localUserContainer) {
        console.error('Stream elements not found.');
        return;
    }

    // Check if the selected stream is already in the local user container
    if (selectedStream.parentElement === localUserContainer) {
        // Move the selected stream back to the remote users container
        remoteUsersContainer.appendChild(selectedStream);
    } else {
        // Move the current host stream back to the remote users container
        const currentHostStream = localUserContainer.firstElementChild;
        if (currentHostStream) {
            remoteUsersContainer.appendChild(currentHostStream);
        }

        // Move the selected stream to the local user container
        localUserContainer.appendChild(selectedStream);
    }
}

document.getElementById("camera-btn").addEventListener("click", toggleCamera);
document.getElementById("mic-btn").addEventListener("click", toggleMic);
document.getElementById("screen-btn").addEventListener("click", toggleScreen);

joinRoomInit();
