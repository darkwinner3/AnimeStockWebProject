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