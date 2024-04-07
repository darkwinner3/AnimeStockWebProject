document.getElementById('CoverImg').addEventListener('change', function () {
    var files = this.files;
    var indicator = document.getElementById('coverImgIndicator');
    if (files.length > 0) {
        indicator.textContent = files[0].name;
    } else {
        indicator.textContent = '';
    }
});

document.getElementById('NewCoverImg').addEventListener('change', function () {
    var files = this.files;
    var indicator = document.getElementById('newCoverImgIndicator');
    if (files.length > 0) {
        indicator.textContent = files[0].name;
    } else {
        indicator.textContent = '';
    }
});

document.getElementById('NewPictures').addEventListener('change', function () {
    var files = this.files;
    var indicator = document.getElementById('newPicturesIndicator');
    if (files.length > 0) {
        indicator.textContent = files.length + ' file(s) selected';
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

//ajax requests for deleting and recovering pictures inside the form
async function deletePicture(pictureId, event) {
    event.preventDefault();

    const url = `/Admin/Picture/Delete?id=${pictureId}`;
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'RequestVerificationToken': csrfToken
            },
        });

        const data = await response.json();

        if (data.success) {
            const buttonClicked = event.target;

            if (buttonClicked.textContent == "Recover") {
                buttonClicked.textContent = 'Delete';
                buttonClicked.setAttribute('onclick', `deletePicture(${pictureId}, event)`);

            }
            else {
                buttonClicked.textContent = 'Recover';
                buttonClicked.setAttribute('onclick', `recoverPicture(${pictureId}, event)`);
            }
        }
    } catch (error) {
        console.error(error);
    }
}

async function recoverPicture(pictureId, event) {
    event.preventDefault();

    const url = `/Admin/Picture/Recover?id=${pictureId}`;
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'RequestVerificationToken': csrfToken
            },
        });

        const data = await response.json();

        if (data.success) {
            const buttonClicked = event.target;

            if (buttonClicked.textContent == "Recover") {
                buttonClicked.textContent = 'Delete';
                buttonClicked.setAttribute('onclick', `deletePicture(${pictureId}, event)`);

            }
            else {
                buttonClicked.textContent = 'Recover';
                buttonClicked.setAttribute('onclick', `recoverPicture(${pictureId}, event)`);
            }
        }
    } catch (error) {
        console.error(error);
    }
}