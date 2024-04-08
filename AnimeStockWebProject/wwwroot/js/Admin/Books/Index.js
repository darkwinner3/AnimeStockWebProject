async function deleteBook(bookId, event) {
    event.preventDefault();

    const url = `/Admin/Book/Delete?id=${bookId}`;
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

            if (buttonClicked.textContent == "Recover Book") {
                buttonClicked.textContent = 'Delete Book';
                buttonClicked.style.backgroundColor = '#dc3545';
                buttonClicked.style.borderColor = '#dc3545';
                buttonClicked.setAttribute('onclick', `deleteBook(${bookId}, event)`);

            }
            else {
                buttonClicked.textContent = 'Recover Book';
                buttonClicked.style.backgroundColor = '#198754';
                buttonClicked.style.borderColor = '#198754';
                buttonClicked.setAttribute('onclick', `recoverBook(${bookId}, event)`);
            }
        }
    } catch (error) {
        console.error(error);
    }
}

async function recoverBook(bookId, event) {
    event.preventDefault();

    const url = `/Admin/Book/Recover?id=${bookId}`;
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

            if (buttonClicked.textContent == "Recover Book") {
                buttonClicked.textContent = 'Delete Book';
                buttonClicked.style.backgroundColor = '#dc3545';
                buttonClicked.style.borderColor = '#dc3545';
                buttonClicked.setAttribute('onclick', `deleteBook(${bookId}, event)`);

            }
            else {
                buttonClicked.textContent = 'Recover Book';
                buttonClicked.style.backgroundColor = '#198754';
                buttonClicked.style.borderColor = '#198754';
                buttonClicked.setAttribute('onclick', `recoverBook(${bookId}, event)`);
            }
        }
    } catch (error) {
        console.error(error);
    }
}