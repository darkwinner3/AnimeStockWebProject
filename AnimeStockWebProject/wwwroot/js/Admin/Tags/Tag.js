async function deleteTag(tagId, event) {
    event.preventDefault();

    const url = `/Admin/BookTag/Delete?id=${tagId}`;
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
                buttonClicked.setAttribute('onclick', `deleteTag(${tagId}, event)`);

            }
            else {
                buttonClicked.textContent = 'Recover';
                buttonClicked.style.backgroundColor = '#198754';
                buttonClicked.style.borderColor = '#198754';
                buttonClicked.setAttribute('onclick', `recoverTag(${tagId}, event)`);
            }
        }
    } catch (error) {
        console.error(error);
    }
}

async function recoverTag(tagId, event) {
    event.preventDefault();

    const url = `/Admin/BookTag/Recover?id=${tagId}`;
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
                buttonClicked.setAttribute('onclick', `deleteTag(${tagId}, event)`);

            }
            else {
                buttonClicked.textContent = 'Recover';
                buttonClicked.style.backgroundColor = '#198754';
                buttonClicked.style.borderColor = '#198754';
                buttonClicked.setAttribute('onclick', `recoverTag(${tagId}, event)`);
            }
        }
    } catch (error) {
        console.error(error);
    }
}