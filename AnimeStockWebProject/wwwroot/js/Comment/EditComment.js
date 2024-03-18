let id = 0; // Variable to store the ID of the currently edited comment

const editButtons = document.querySelectorAll(".button-container .edit");

editButtons.forEach(button => {
    button.addEventListener('click', editComment);
});

function editComment(event) {
    // Get the comment card associated with the clicked edit button
    const commentElement = event.target.closest(".comment-card");
    // Get the comment text element within the comment card
    const commentTextElement = commentElement.querySelector(".comment");

    // Ensure only one comment is editable at a time
    if (!commentTextElement.isContentEditable) {
        // If the comment is not currently editable, make it editable
        commentTextElement.contentEditable = true;
        commentTextElement.focus();
        event.target.textContent = "Save"; // Change button text to "Save" during editing

        // Retrieve the ID of the edited comment
        id = parseInt(commentElement.dataset.commentId); // Assuming you have a data attribute for comment ID
    } else {
        // If the comment is currently editable, save the edited comment

        const editedComment = commentTextElement.textContent.trim();

        const url = '/Comment/Edit';

        let commentData = {
            Description: editedComment,
            Id: id
        };

        fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': csrfToken
            },
            body: JSON.stringify(commentData)
        })
            .then(response => response.json())
            .then(data => {
                if (data.ok) {
                    window.location.reload();
                } else if (data.errors) {
                    let errorParagraph = document.createElement("p");
                    errorParagraph.classList.add("error-paragraph");
                    let object = data.errors[0];
                    errorParagraph.textContent = object.error;
                    event.target.parentElement.appendChild(errorParagraph);
                } else {
                    window.location.reload();
                }
            })
            .catch(error => {
                console.error(error);
            });

        // Reset state after editing
        id = 0;
        commentTextElement.contentEditable = false;
        event.target.textContent = "Edit";
    }
}