const userInfoPanel = document.getElementById("userInfoPanel");
const overlay = document.querySelector(".overlay");

ShowUserInfo =()=>{
    userInfoPanel.classList.toggle("showInfo");
    overlay.style.display = "block";
}
HideUserInfo=()=> {
    userInfoPanel.classList.remove("showInfo");
    overlay.style.display = "none";
}

