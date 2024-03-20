const favoriteElements = Array.from(document.querySelectorAll('.favorite-container'))
    .filter((elemet) => elemet.textContent.match("Favorite"));
favoriteElements.forEach((element) => element.addEventListener("click", addToFavorites));

const unFavoriteElements = Array.from(document.querySelectorAll('.favorite-container'))
    .filter((elemet) => elemet.textContent.match("Unfavorite"));
unFavoriteElements.forEach((element) => element.addEventListener("click", removeFromFavorites));

async function addToFavorites(event) {
    console.log(csrfToken);
    const bookId = event.target.parentElement.children[5].value;
    const url = `/Book/AddToFavorites/${bookId}`;

    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'RequestVerificationToken': csrfToken
            },
        });

        if (response.ok) {
            window.location.reload();
        }
        else {
            console.error('Response error:', response.status);
        }

    } catch (e) {
        console.error(exception);
    }
}

async function removeFromFavorites(event) {
    const bookId = event.target.parentElement.children[5].value;
    const url = `/Book/RemoveFromFavorites/${bookId}`;

    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'RequestVerificationToken': csrfToken
            },
        });

        if (response.ok) {
            window.location.reload();
        }
        else {
            console.error('Response error:', response.status);
        }

    } catch (e) {
        console.error(exception);
    }
}

function selectPrintType(printType) {
    var printTypeRadios = document.querySelectorAll('input.sort-option');
    printTypeRadios.forEach(function (radio) {
        var label = radio.parentElement.innerText.trim();
        if (label === printType) {
            radio.checked = true;
            radio.dispatchEvent(new Event('change')); // Trigger change event
            return;
        }
    });
}

function selectBookType(bookType) {
    var bookTypeOption = document.querySelectorAll('input.checkBox');
    bookTypeOption.forEach(function (checkBox) {
        var label = checkBox.parentElement.innerText.trim();
        if (label === bookType) {
            checkBox.checked = true;
            checkBox.dispatchEvent(new Event('change')); // Trigger change event
            return;
        }
    });
}

