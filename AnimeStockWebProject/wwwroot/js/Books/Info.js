
document.addEventListener("DOMContentLoaded", function () {
    const imgElements = document.querySelectorAll(".img-container img");
    const prevButton = document.querySelector(".prev-button");
    const nextButton = document.querySelector(".next-button");
    const previewImages = document.querySelectorAll(".preview-active-img");
    let currentIndex = 0;

    // Show initial active image
    imgElements[currentIndex].classList.add("active-img");
    previewImages[currentIndex].classList.add("active-preview");

    // Function to show next image
    function showNextImage() {
        imgElements[currentIndex].classList.remove("active-img");
        previewImages[currentIndex].classList.remove("active-preview");

        currentIndex++;
        if (currentIndex >= imgElements.length) {
            currentIndex = 0;
        }
        imgElements[currentIndex].classList.add("active-img");
        previewImages[currentIndex].classList.add("active-preview");
    }

    // Function to show previous image
    function showPrevImage() {
        imgElements[currentIndex].classList.remove("active-img");
        previewImages[currentIndex].classList.remove("active-preview");
        currentIndex--;
        if (currentIndex < 0) {
            currentIndex = imgElements.length - 1;
        }
        imgElements[currentIndex].classList.add("active-img");
        previewImages[currentIndex].classList.add("active-preview");
    }

    // Event listener for next button
    nextButton.addEventListener("click", showNextImage);

    // Event listener for previous button
    prevButton.addEventListener("click", showPrevImage);
    previewImages.forEach(function (previewImg, index) {
        previewImg.addEventListener("click", function () {
            imgElements[currentIndex].classList.remove("active-img");
            previewImages[currentIndex].classList.remove("active-preview");
            currentIndex = index;
            imgElements[currentIndex].classList.add("active-img");
            previewImg.classList.add("active-preview");
        });
    });
    
});
//get necessary info for suggested books
const titleTypeElement = document.querySelector('.book-title');
const titleType = titleTypeElement ? titleTypeElement.textContent.trim() : ''; // Check if the element exists before accessing its text content

// Get current URL
const currentUrl = new URL(window.location.href);
// Get the last element from the URL path
const bookId = currentUrl.pathname.split('/').pop();

const baseUrl = '/Book/BooksByTitle?';

getBooksByTitle(titleType, baseUrl, bookId);
//ajax function for getting the books
async function getBooksByTitle(title, baseUrl, id) {
    const data = {
        title: title,
        id: id
    };

    const queryString = Object.entries(data)
        .map(([key, value]) => encodeURIComponent(key) + "=" + encodeURIComponent(value))
        .join("&");

    try {
        const response = await fetch(baseUrl + queryString);

        if (response.ok) {
            const responseData = await response.text();
            const otherBooksContainer = document.getElementById("other-books");
            if (otherBooksContainer) {
                otherBooksContainer.innerHTML += responseData;

                const childrenCount = otherBooksContainer.children.length;
                const noBooks = document.querySelector(".no-suggestions");
                noBooks.style.display = 'block';
                if (childrenCount > 1) {
                    noBooks.style.display = 'none';
                    createSlider();
                }
            }
        } else {
            console.error('Response error:', response.status);
        }
    } catch (error) {
        console.error(error);
    }
}
//create slider for suggested books
function createSlider() {
    const initialCountCards = 5;
    let currentIndex = 0;
    const cards = [...document.querySelectorAll('#other-books .recomended-book-card')];

    //showing amount of containers
    for (var i = 0; i < initialCountCards; i++) {
        if (cards[i]) {
            cards[i].style.display = 'flex';
        }
    }

    const leftArrowBtn = document.querySelector('.sugestion-item-container .left-button');
    const rightArrowBtn = document.querySelector('.sugestion-item-container .right-button');

    rightArrowBtn.addEventListener('click', () => {
        //hiding containers
        for (var i = currentIndex; i < initialCountCards + currentIndex; i++) {
            if (cards[i]) {
                cards[i].style.display = 'none';
            }
        }

        if (currentIndex >= cards.length) {
            currentIndex = 0;
        }
        //showing current containers
        for (var i = currentIndex; i < initialCountCards + currentIndex; i++) {
            if (cards[i]) {
                cards[i].style.display = 'flex';
            }
        }
    });

    leftArrowBtn.addEventListener('click', () => {
        let initialIndex = currentIndex;

        if (initialIndex - initialCountCards >= 0) {
            initialIndex -= initialCountCards;
        }
        else {
            initialIndex = cards.length - initialCountCards;
        }

        //hiding current containers
        for (var i = currentIndex; i < currentIndex + initialCountCards; i++) {
            if (cards[i]) {
                cards[i].style.display = 'none';
            }
        }

        //showing last containers
        for (var i = initialIndex; i < initialIndex + initialCountCards; i++) {
            if (cards[i]) {
                cards[i].style.display = 'flex';
            }
        }
        currentIndex = initialIndex;
    });
}

document.getElementById('book-details').addEventListener('click', function (event) {
    event.preventDefault(); // Prevent default anchor behavior

    // Get the target element by its href attribute
    const targetSelector = this.getAttribute('href');

    // Get the first element with the target class
    const targetElement = document.querySelector(targetSelector);

    if (targetElement) {
        // Calculate the offset of the target element relative to the viewport
        const offsetTop = targetElement.getBoundingClientRect().top + window.pageYOffset;

        // Smooth scroll to the target element
        window.scrollTo({
            top: offsetTop,
            behavior: 'smooth'
        });
    }
});

//remove and add from favorites
document.addEventListener("DOMContentLoaded", function () {
    const favoriteButtons = Array.from(document.querySelectorAll('#favorite-container'));
    favoriteButtons.forEach((button) => button.addEventListener("click", toggleFavorite));
});

async function toggleFavorite(event) {
    const button = event.target;
    const bookId = button.parentElement.querySelector('input[type="hidden"]').value;
    const url = button.textContent === "Favorite" ? `/Book/AddToFavorites/${bookId}` : `/Book/RemoveFromFavorites/${bookId}`;

    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'RequestVerificationToken': csrfToken
            },
        });

        if (response.ok) {
            window.location.reload();
        } else {
            console.error('Response error:', response.status);
        }

    } catch (error) {
        console.error(error);
    }
}
//ajax function for book preview
document.getElementById('preview-button').addEventListener('click', async function () {
    const bookId = event.target.parentElement.querySelector('input[type="hidden"]').value;
    const filePath = document.querySelector('input[type="hidden"]').value;
    const pageCount = 30;
    //page url
    const url = `/Book/BookPartial?id=${bookId}&pageCount=${pageCount}&filePath=${filePath}`;

    try {
        const response = await fetch(url);
        if (response.ok) {
            const pdfContent = await response.blob();
            renderPdf(pdfContent);
            document.getElementById('pdf-overlay').style.display = 'block';
        } else {
            console.error('Response error:', response.status);
        }
    } catch (error) {
        console.error('Fetch error:', error);
    }
});

function renderPdf(pdfBlob) {
    const pdfUrl = URL.createObjectURL(pdfBlob);
    const container = document.getElementById('pdf-container');
    container.innerHTML = ''; // Clear previous content

    // Initialize PDF.js
    pdfjsLib.getDocument(pdfUrl).promise.then(function (pdfDoc) {
        const numPages = pdfDoc.numPages;

        // Render each page
        for (let i = 1; i <= numPages; i++) {
            pdfDoc.getPage(i).then(function (page) {
                const canvas = document.createElement('canvas');
                const context = canvas.getContext('2d');

                const viewport = page.getViewport({ scale: 1.5 });

                canvas.height = viewport.height;
                canvas.width = viewport.width;

                const renderContext = {
                    canvasContext: context,
                    viewport: viewport
                };

                page.render(renderContext).promise.then(function () {
                    container.appendChild(canvas);
                });
            });
        }
    });
}

// Close the overlay when clicking outside the PDF container
document.getElementById('pdf-overlay').addEventListener('click', function (event) {
    if (event.target === this) {
        document.getElementById('pdf-overlay').style.display = 'none'; // Hide the overlay
    }
});