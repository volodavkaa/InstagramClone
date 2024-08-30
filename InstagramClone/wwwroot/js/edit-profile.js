document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("editProfileModal");
    var btn = document.getElementById("editProfileButton");
    var span = document.getElementsByClassName("close")[0];

    
    btn.onclick = function () {
        modal.style.display = "block";
    };

    
    span.onclick = function () {
        modal.style.display = "none";
    };

   
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    };

    
    var profileBio = document.getElementById("profileBio");
    var charCount = document.getElementById("charCount");

    profileBio.addEventListener("input", function () {
        var remaining = 800 - profileBio.value.length;
        charCount.textContent = "Залишилось " + remaining + " символів";
    });
});
