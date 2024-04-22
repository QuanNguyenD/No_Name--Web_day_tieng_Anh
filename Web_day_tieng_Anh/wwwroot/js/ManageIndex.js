document.getElementById('imageInput').addEventListener('change', function (e) {
    var output = document.getElementById('previewImage');
    if (this.files && this.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            output.src = e.target.result;
            output.style.display = 'block';
        }

        reader.readAsDataURL(this.files[0]);
    }
})

// Add event listener to the profile image to trigger file input click

