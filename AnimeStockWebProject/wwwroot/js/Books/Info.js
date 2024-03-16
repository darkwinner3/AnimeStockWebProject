document.addEventListener("DOMContentLoaded", function () {
    const imgElements = document.querySelectorAll(".img-container img");
    const prevButton = document.querySelector(".prev-button");
    const nextButton = document.querySelector(".next-button");
    let currentIndex = 0;

    // Show initial active image
    imgElements[currentIndex].classList.add("active-img");

    // Function to show next image
    function showNextImage() {
        imgElements[currentIndex].classList.remove("active-img");
        currentIndex++;
        if (currentIndex >= imgElements.length) {
            currentIndex = 0;
        }
        imgElements[currentIndex].classList.add("active-img");
    }

    // Function to show previous image
    function showPrevImage() {
        imgElements[currentIndex].classList.remove("active-img");
        currentIndex--;
        if (currentIndex < 0) {
            currentIndex = imgElements.length - 1;
        }
        imgElements[currentIndex].classList.add("active-img");
    }

    // Event listener for next button
    nextButton.addEventListener("click", showNextImage);

    // Event listener for previous button
    prevButton.addEventListener("click", showPrevImage);
    
});

const titleTypeElement = document.querySelector('.book-title');
const titleType = titleTypeElement ? titleTypeElement.textContent.trim() : ''; // Check if the element exists before accessing its text content

// Get current URL
const currentUrl = new URL(window.location.href);
// Get the last element from the URL path
const bookId = currentUrl.pathname.split('/').pop();

const baseUrl = '/Book/BooksByTitle?';

getBooksByTitle(titleType, baseUrl, bookId);

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
                const bookSuggestion = document.querySelector(".book-suggestion");
                if (childrenCount === 0 && bookSuggestion) {
                    bookSuggestion.style.display = 'none';
                    const [leftArrow, rightArrow] = document.querySelectorAll('.sugestion-item-container button');
                    leftArrow.style.display = 'none';
                    rightArrow.style.display = 'none';
                } else if (bookSuggestion) {
                    bookSuggestion.style.display = 'block';
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

    const leftArrowBtn = document.querySelector('.sugestion-item-container .left-arrow');
    const rightArrowBtn = document.querySelector('.sugestion-item-container .right-arrow');

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
