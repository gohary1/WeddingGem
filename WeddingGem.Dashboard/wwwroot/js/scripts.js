

window.addEventListener('DOMContentLoaded', event => {

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sb-sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }
    document.querySelector('.close').addEventListener('click', function () {
        document.querySelector('.popUp').classList.add('d-none');
    });

    document.querySelector('tbody').addEventListener('click', function (event) {
        if (event.target.classList.contains('delete')) {
            const userId = event.target.getAttribute('data-id');
            const username = event.target.getAttribute('data-username');
            document.getElementById('delete-message').textContent = `Are You Sure to Delete ${username}?`;
            document.getElementById('confirm-delete').setAttribute('@asp-route-id', `${userId}`);
            document.querySelector('.popUp').classList.remove('d-none');
        }
    });
    document.querySelector('.deleteConfirm').addEventListener('click', function () {
        document.querySelector('.popUp').classList.add('d-none');
    });

});
