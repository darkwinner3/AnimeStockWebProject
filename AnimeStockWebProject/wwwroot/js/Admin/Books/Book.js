var coverImgElement = document.getElementById('CoverImg');
if (coverImgElement) {
    coverImgElement.addEventListener('change', function () {
        var files = this.files;
        var indicator = document.getElementById('coverImgIndicator');
        if (files.length > 0) {
            indicator.textContent = files[0].name;
        } else {
            indicator.textContent = '';
        }
    });
}

var NewCoverImgElement = document.getElementById('NewCoverImg');
if (NewCoverImgElement) {
    NewCoverImgElement.addEventListener('change', function () {
        var files = this.files;
        var indicator = document.getElementById('newCoverImgIndicator');
        if (files.length > 0) {
            indicator.textContent = files[0].name;
        } else {
            indicator.textContent = '';
        }
    });
}

var NewPicturesElement = document.getElementById('NewPictures');
if (NewPicturesElement) {
    NewPicturesElement.addEventListener('change', function () {
        var files = this.files;
        var indicator = document.getElementById('newPicturesIndicator');
        if (files.length > 0) {
            indicator.textContent = files.length + ' file(s) selected';
        } else {
            indicator.textContent = '';
        }
    });
}

var picturesElement = document.getElementById('Pictures');
if (picturesElement) {
    picturesElement.addEventListener('change', function () {
        var files = this.files;
        var indicator = document.getElementById('picturesIndicator');
        if (files.length > 0) {
            indicator.textContent = files.length + ' file(s) selected';
        } else {
            indicator.textContent = '';
        }
    });
}

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
                buttonClicked.style.backgroundColor = '#dc3545';
                buttonClicked.style.borderColor = '#dc3545';
                buttonClicked.setAttribute('onclick', `deletePicture(${pictureId}, event)`);

            }
            else {
                buttonClicked.textContent = 'Recover';
                buttonClicked.style.backgroundColor = '#198754';
                buttonClicked.style.borderColor = '#198754';
                buttonClicked.setAttribute('onclick', `recoverPicture(${pictureId}, event)`);
            }
        }
        else if (data.isNotDeleted) {
            window.location.reload();
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
                buttonClicked.style.backgroundColor = '#dc3545';
                buttonClicked.style.borderColor = '#dc3545';
                buttonClicked.setAttribute('onclick', `deletePicture(${pictureId}, event)`);

            }
            else {
                buttonClicked.textContent = 'Recover';
                buttonClicked.style.backgroundColor = '#198754';
                buttonClicked.style.borderColor = '#198754';
                buttonClicked.setAttribute('onclick', `recoverPicture(${pictureId}, event)`);
            }
        }
    } catch (error) {
        console.error(error);
    }
}