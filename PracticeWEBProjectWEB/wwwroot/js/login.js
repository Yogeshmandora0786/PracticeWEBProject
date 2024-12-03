function Login(event) {
    event.preventDefault(); // Prevent form submission

    // Get user input
    const username = document.getElementById('username').value.trim();
    const password = document.getElementById('password').value.trim();

    // Validate inputs
    if (!username || !password) {
        Swal.fire({
            icon: 'warning',
            title: 'Missing Fields',
            text: 'Please enter both username and password!',
        });
        return;
    }

    // Create FormData object to send the login data
    const formData = new FormData();
    formData.append('Username', username);
    formData.append('Password', password);

    // Show loading alert
    Swal.fire({
        title: 'Logging In...',
        text: 'Please wait while we process your request.',
        allowOutsideClick: false,
        showConfirmButton: false,
        didOpen: () => {
            Swal.showLoading();
        },
    });

    // Make the AJAX request
    $.ajax({
        url: '/Login/LoginPost',
        type: 'POST',
        data: formData,
        contentType: false,  // Let the browser set the content type
        processData: false,  // Let jQuery handle the FormData
        success: function (response) {
            setTimeout(() => { // Set timeout to display the loader for a minimum time
                Swal.close(); // Close the loader after a delay
                if (response.responseCode === 200) {
                    console.log("Redirecting to /Home/Index");
                    window.location.href = '/Home/Index';
                } else {
                    console.error("Invalid Login:", response);
                    Swal.fire({
                        icon: 'error',
                        title: 'Login Failed',
                        text: response.responseMessage,
                    });
                }
            }, 1000); // Loader will show for at least 1 second before closing
        },
        error: function (xhr, status, error) {
            setTimeout(() => {
                Swal.close(); // Close the loader after a delay
                console.error("AJAX Error:", status, error);
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'An error occurred during the login process.',
                });
            }, 1000); // Loader will show for at least 1 second before closing
        }
    });
}

