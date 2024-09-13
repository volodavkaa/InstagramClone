document.addEventListener("DOMContentLoaded", function () {
    const postImage = document.getElementById("postImage");
    const postContent = document.getElementById("postContent");
    const submitPostBtn = document.getElementById("submitPostBtn");

    function toggleSubmitButton() {
        if (postImage.files.length > 0 && postContent.value.trim() !== "") {
            submitPostBtn.disabled = false;
        } else {
            submitPostBtn.disabled = true;
        }
    }

    postImage.addEventListener("change", toggleSubmitButton);
    postContent.addEventListener("input", toggleSubmitButton);
});
