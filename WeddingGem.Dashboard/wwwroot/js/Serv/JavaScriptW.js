window.addEventListener('DOMContentLoaded', event => {

    // Close button event listener
    document.querySelector('.close').addEventListener('click', function () {
        document.querySelector('.popUp').classList.add('d-none');
    });

    // Event delegation for delete buttons
    document.querySelector('tbody').addEventListener('click', function (event) {
        if (event.target.classList.contains('delete')) {
            const userId = event.target.getAttribute('data-id');
            const username = event.target.getAttribute('data-username');
            const baseHref = event.target.getAttribute('data-delete-url');
            document.getElementById('delete-message').textContent = `Are You Sure to Delete ${username}?`;
            document.getElementById('confirm-delete').setAttribute('href', `${baseHref}/${userId}`);
            document.querySelector('.popUp').classList.remove('d-none');
        }
    });
    document.querySelector('tbody').addEventListener('click', function (event) {
        if (event.target.classList.contains('accept')) {
            const userId = event.target.getAttribute('data-id');
            const username = event.target.getAttribute('data-username');
            const baseHref = event.target.getAttribute('data-delete-url');
            document.getElementById('delete-message').textContent = `Are You Sure to Accept ${username}?`;
            document.getElementById('confirm-delete').setAttribute('href', `${baseHref}/${userId}`);
            document.querySelector('.popUp').classList.remove('d-none');
        }
    });

    // Confirm delete button event listener
    document.querySelector('.deleteConfirm').addEventListener('click', function () {
        document.querySelector('.popUp').classList.add('d-none');
    });

    $(document).ready(function () {
        if ($("#errorMessage").length > 0) {
            setTimeout(function () {
                $("#errorMessage").fadeOut();
            }, 4000); 
        }
    });
});
