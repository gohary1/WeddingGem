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
            document.getElementById('delete-message').textContent = `Are You Sure to Delete ${username}?`;
            document.getElementById('confirm-delete').setAttribute('href', `/User/Delete/${userId}`);
            document.querySelector('.popUp').classList.remove('d-none');
        }
    });

    // Confirm delete button event listener
    document.querySelector('.deleteConfirm').addEventListener('click', function () {
        document.querySelector('.popUp').classList.add('d-none');
    });
});
