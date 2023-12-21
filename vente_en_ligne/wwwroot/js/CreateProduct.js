
    function chooseFile() {
        document.getElementById('imageFile').click();
                }

    function handleImageSelect(input) {
                    const file = input.files[0];

    if (file) {
                        const imgElement = document.getElementById('imagePreview');

    // Display the selected image preview
    imgElement.src = URL.createObjectURL(file);
    imgElement.style.display = 'block';
                    }
                }
