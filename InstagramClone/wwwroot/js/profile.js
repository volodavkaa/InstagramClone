document.addEventListener('DOMContentLoaded', function () {
    const profilePictureElement = document.querySelector('.profile-picture img');
    const profilePictureInput = document.getElementById('profilePicture');

    profilePictureElement.addEventListener('click', function () {
        profilePictureInput.click();
    });

    profilePictureInput.addEventListener('change', function () {
        const file = profilePictureInput.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                profilePictureElement.src = e.target.result;
            }
            reader.readAsDataURL(file);
        }
    });
});
