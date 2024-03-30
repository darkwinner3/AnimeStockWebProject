document.addEventListener('DOMContentLoaded', function () {
    if (document.getElementById('errorModal')) {
        if (document.getElementById('errorModal').querySelector('.modal-title').textContent.trim() !== '') {
            $('#errorModal').modal('show');

            setTimeout(function () {
                $('#errorModal').modal('hide');
            }, 2000);
        }
    }

    if (document.getElementById('successModal')) {
        if (document.getElementById('successModal').querySelector('.modal-title').textContent.trim() !== '') {
            $('#successModal').modal('show');
            setTimeout(function () {
                $('#successModal').modal('hide');
            }, 2000);

        }
    }

    if (document.getElementById('warningModal')) {
        if (document.getElementById('warningModal').querySelector('.modal-title').textContent.trim() !== '') {
            $('#warningModal').modal('show');

            setTimeout(function () {
                $('#warningModal').modal('hide');
            }, 2000);
        }
    }
});