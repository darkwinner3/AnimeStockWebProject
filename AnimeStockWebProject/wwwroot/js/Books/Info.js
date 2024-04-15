
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

var touchStartX = 0;
var touchEndX = 0;
var imgIndex = 0;
var images = document.querySelectorAll('.img img');
var numImages = images.length;

// Add event listeners to each image
images.forEach(function (image, index) {
    image.addEventListener('touchstart', function (event) {
        touchStartX = event.touches[0].clientX;
    });

    image.addEventListener('touchmove', function (event) {
        touchEndX = event.touches[0].clientX;
    });

    image.addEventListener('touchend', function () {
        if (touchStartX - touchEndX > 50) {
            // Swipe left
            imgIndex = (index + 1) % numImages;
        } else if (touchEndX - touchStartX > 50) {
            // Swipe right
            imgIndex = (index - 1 + numImages) % numImages;
        }
        showImage(imgIndex);
    });
});

function showImage(index) {
    images.forEach(function (image, i) {
        if (i !== index) {
            image.style.display = 'none';
        } else {
            image.style.display = 'block';
        }
    });
}

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
                noBooks.style.display = 'flex';
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
    const cards = [...document.querySelectorAll('#other-books .recomended-book-card')];
    const cardCount = cards.length;
    const cardsPerSlide = 3;
    let currentSlideIndex = 0;

    const updateDisplay = () => {
        const startIndex = currentSlideIndex * cardsPerSlide;
        const endIndex = Math.min(startIndex + cardsPerSlide, cardCount);

        cards.forEach((card, index) => {
            if (index >= startIndex && index < endIndex) {
                card.style.display = 'flex';
            } else {
                card.style.display = 'none';
            }
        });
    };

    const showNextSlide = () => {
        currentSlideIndex = (currentSlideIndex + 1) % Math.ceil(cardCount / cardsPerSlide);
        updateDisplay();
    };

    const showPreviousSlide = () => {
        currentSlideIndex = (currentSlideIndex - 1 + Math.ceil(cardCount / cardsPerSlide)) % Math.ceil(cardCount / cardsPerSlide);
        updateDisplay();
    };

    const leftArrowBtn = document.querySelector('.sugestion-item-container .left-button');
    const rightArrowBtn = document.querySelector('.sugestion-item-container .right-button');

    rightArrowBtn.addEventListener('click', showNextSlide);
    leftArrowBtn.addEventListener('click', showPreviousSlide);

    // Initial display setup
    updateDisplay();
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
    const bookId = document.getElementById('bookId').value;
    const filePath = document.getElementById('book-path').value;
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

//UNFINISHED PDF RENDERING! WAS GONNA ALLOW FOR FONT CHANGES THROUGH A MENU

//async function renderPdf(pdfBlob) {
//    const pdfUrl = URL.createObjectURL(pdfBlob);
//    const container = document.getElementById('pdf-container');
//    container.innerHTML = ''; // Clear previous content

//    // Initialize PDF.js
//    const pdfDoc = await pdfjsLib.getDocument(pdfUrl).promise;
//    const numPages = pdfDoc.numPages;

//    // Render each page
//    for (let i = 1; i <= numPages; i++) {
//        const page = await pdfDoc.getPage(i);
//        const textContent = await page.getTextContent();
//        const viewport = page.getViewport({ scale: 1.5 });

//        // Create a container for each page
//        const pageContainer = document.createElement('div');
//        pageContainer.className = 'pdf-page-container'; // Apply CSS styles to this class if needed

//        // Append the page container to the main container
//        container.appendChild(pageContainer);

//        // Render text content into the page container
//        textContent.items.forEach(function (textItem) {
//            const textNode = document.createTextNode(textItem.str + ' ');
//            const span = document.createElement('span');
//            span.style.fontFamily = 'YourFont'; // Set the desired font
//            // You can apply other styles here if needed, e.g., font size, color, etc.
//            span.appendChild(textNode);
//            pageContainer.appendChild(span);
//        });

//        // Check for images and render them
//        const operatorList = await page.getOperatorList();
//        const svgGfx = new pdfjsLib.SVGGraphics(page.commonObjs, page.objs);
//        const svg = await svgGfx.getSVG(operatorList, viewport);

//        // Create a div to hold images for this page
//        const imagesDiv = document.createElement('div');
//        pageContainer.appendChild(imagesDiv);

//        // Look for SVG images and replace them with img elements
//        svg.querySelectorAll('image').forEach(async function (image) {
//            const imageData = image.getAttribute('xlink:href');
//            const img = document.createElement('img');
//            img.src = imageData;
//            imagesDiv.appendChild(img);
//        });
//    }
//}

//SECOND TYPE OF PDF RENDERING! CHANGE TO IT IF FIRST ONE LOOKS BAD
async function renderPdf(pdfBlob) {
    const pdfUrl = URL.createObjectURL(pdfBlob);
    const container = document.getElementById('pdf-container');
    container.innerHTML = ''; // Clear previous content

    // Initialize PDF.js
    const pdfDoc = await pdfjsLib.getDocument(pdfUrl).promise;
    const numPages = pdfDoc.numPages;

    // Render each page
    for (let i = 1; i <= numPages; i++) {
        const page = await pdfDoc.getPage(i);
        const viewport = page.getViewport({ scale: 1.5 });

        // Check if the page contains images
        const operatorList = await page.getOperatorList();
        const svgGfx = new pdfjsLib.SVGGraphics(page.commonObjs, page.objs);
        const svg = await svgGfx.getSVG(operatorList, viewport);
        const images = svg.querySelectorAll('image');

        // Create a container for the page content
        const pageContainer = document.createElement('div');
        pageContainer.className = 'pdf-page-container';
        container.appendChild(pageContainer);

        // If the page contains images, render them as <img> elements
        if (images.length > 0) {
            images.forEach(function (image) {
                const imageData = image.getAttribute('xlink:href');
                const img = document.createElement('img');
                img.src = imageData;
                pageContainer.appendChild(img);
            });
        } else {
            // Otherwise, render the page as canvas
            const canvas = document.createElement('canvas');
            const context = canvas.getContext('2d');
            canvas.height = viewport.height;
            canvas.width = viewport.width;
            const renderContext = {
                canvasContext: context,
                viewport: viewport
            };
            await page.render(renderContext).promise;
            pageContainer.appendChild(canvas);
        }
    }
}


// Close the overlay when clicking outside the PDF container
document.getElementById('pdf-overlay').addEventListener('click', function (event) {
    if (event.target === this) {
        document.getElementById('pdf-overlay').style.display = 'none'; // Hide the overlay
    }
});