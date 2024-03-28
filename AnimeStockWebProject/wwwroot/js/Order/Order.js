document.addEventListener("DOMContentLoaded", function () {
    const downloadButton = document.querySelector('.download-button');
    downloadButton.addEventListener("click", Download);
});

async function Download(event) {
    const orderId = document.getElementById('orderId').value;
    const url = `/Order/OrderInfo/${orderId}`;
    const filePath = document.querySelector("#fileName").value;
    const fileName = filePath.split("/").pop();

    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'RequestVerificationToken': csrfToken
            },
        });

        if (response.ok) {
            const fileDetails = await response.blob();
            const fileUrl = window.URL.createObjectURL(fileDetails);
            const link = document.createElement('a');
            link.href = fileUrl;
            link.download = fileName;
            link.click();
        } else {
            console.error('Response error:', response.status);
        }

    } catch (error) {
        console.error(error);
    }
}