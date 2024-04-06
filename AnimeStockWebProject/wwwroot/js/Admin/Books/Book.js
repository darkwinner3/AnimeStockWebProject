document.getElementById('CoverImg').addEventListener('change', function () {
    var files = this.files;
    var indicator = document.getElementById('coverImgIndicator');
    if (files.length > 0) {
        indicator.textContent = files[0].name;
    } else {
        indicator.textContent = '';
    }
});

document.getElementById('Pictures').addEventListener('change', function () {
    var files = this.files;
    var indicator = document.getElementById('picturesIndicator');
    if (files.length > 0) {
        indicator.textContent = files.length + ' file(s) selected';
    } else {
        indicator.textContent = '';
    }
});

document.getElementById('BookFile').addEventListener('change', function () {
    var files = this.files;
    var indicator = document.getElementById('bookFileIndicator');
    if (files.length > 0) {
        indicator.textContent = files[0].name;
    } else {
        indicator.textContent = '';
    }
});


function toggleBookQuantityVisibility() {
    var digitalPrintType = document.getElementById("DigitalPrintType");
    var quantityFormGroup = document.getElementById("quantity-form");

    if (digitalPrintType.checked) {
        quantityFormGroup.style.display = "none";
        document.getElementById("Quantity").value = 0;
    } else {
        quantityFormGroup.style.display = "block";
    }
}

document.getElementById("DigitalPrintType").addEventListener("change", toggleBookQuantityVisibility);
document.getElementById("PhysicalPrintType").addEventListener("change", toggleBookQuantityVisibility);

toggleBookQuantityVisibility();

//ajax request for for deleting pictures inside the form
function deletePicture(imgId) {
    const url = `Admin/Picture/Delete/${imgId}`;

    fetch(url, {
        method: 'POST',
        headers: { 'RequestVerificationToken': csrfToken },
    })
        .then(Response => Response.json())
        .then(data => {
            if (data.success) {
                const deletedImg = document.getElementById(`image_${imgId}`);
                if (deletedImg) {
                    deletedImg.parentNode.removeChild(deletedImg);
                }
            }
        })
        .catch((error) => console.error(error));

}