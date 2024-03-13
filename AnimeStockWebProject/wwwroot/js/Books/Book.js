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

