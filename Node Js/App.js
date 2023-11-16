// var btn = document.getElementById('btn_ok');
// var val = document.getElementById('roomid').val();

// btn.onclick(function(){
//     var meetingUrl = window.location.origin + "? meetingId = " + val;
//     window.location.replace(meetingUrl);
// })

var localStream;

function data(userid , roomid){
        
    console.log('userid : ', userid);
    console.log('roomid : ', roomid);
}
let init = async () =>{

    
    const url = new URLSearchParams(window.location.search).get('meetingId');
    console.log(url);
    var userId = prompt('Enter name')
     
    localStream = await navigator.mediaDevices.getUserMedia({video:true , audio:false});
    document.getElementById('user1').srcObject = localStream;
    data(userId, url);
}

init();

