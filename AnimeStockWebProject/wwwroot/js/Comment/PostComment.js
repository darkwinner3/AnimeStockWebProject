const postButton = document.querySelector(".post-button");
const declineButton = document.querySelector(".decline-button");
const commentElement = document.querySelector(".comment-editable");
postButton.addEventListener("click", postComment);
//Get Url
const url = new URL(window.location.href);

let Id = url.pathname.split('/').pop();

async function postComment() {
    
    // Prepare the comment data
    let commentData = {
        Description: commentElement.innerText.trim(), // Use innerText to get plain text content
        BookId: Id
    };

    // URL for posting the comment
    const url = "/Comment/PostComment";

    // Post the comment data to the server
    fetch(url, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': csrfToken
            },
            body: JSON.stringify(commentData)
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                window.location.reload();
            } else if (data.errors) {
                let errorObject = data.errors[0];
                console.log(errorObject);
                // Handle errors appropriately
                commentElement.textContent += " " + errorObject.error;
                commentElement.style.color = "red";
                setTimeout(() => {
                    window.location.reload();
                }, 1500);
            } else {
                window.location.reload();
            }
        })
        .catch((error) => console.error(error));
}

declineButton.addEventListener("click", () => {
    // Clear the content of the comment-editable element
    commentElement.textContent = '';
});