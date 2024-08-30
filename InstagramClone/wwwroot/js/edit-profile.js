document.addEventListener("DOMContentLoaded", function () {
    var modal = document.getElementById("editProfileModal");
    var btn = document.getElementById("editProfileButton");
    var span = document.getElementsByClassName("close")[0];

    // Відкрити модальне вікно при натисканні на кнопку
    btn.onclick = function () {
        modal.style.display = "block";
    };

    // Закрити модальне вікно при натисканні на хрестик
    span.onclick = function () {
        modal.style.display = "none";
    };

    // Закрити модальне вікно при натисканні на будь-яке місце поза ним
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    };

    // Оновлювати лічильник символів для поля біографії
    var profileBio = document.getElementById("profileBio");
    var charCount = document.getElementById("charCount");

    profileBio.addEventListener("input", function () {
        var remaining = 800 - profileBio.value.length;
        charCount.textContent = "Залишилось " + remaining + " символів";
    });
});
