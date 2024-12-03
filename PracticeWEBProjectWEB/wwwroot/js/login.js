function SignIn(event) {
    event.preventDefault(); // Prevent default form submission

    // Get form values
    const username = document.getElementById('username').value.trim();
    const password = document.getElementById('password').value.trim();

    // Validate input fields
    if (!username || !password) {
        Swal.fire({
            icon: 'warning',
            title: 'Missing Fields',
            text: 'Both username and password are required!',
        });
        return;
    }

    // Prepare payload
    const payload = {
        Username: username,
        Password: password,
    };

    // Show loading alert
    Swal.fire({
        title: 'Logging In...',
        text: 'Please wait while we log you in.',
        allowOutsideClick: false,
        showConfirmButton: false,
        didOpen: () => {
            Swal.showLoading();
        },
    });

    // Make the API call using fetch
    fetch('https://localhost:7146/api/Login/Login_Upsert', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(payload),
    })
        .then(async (response) => {
            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Invalid username or password.');
            }
            return response.json();
        })
        .then((data) => {
            Swal.close();
            Swal.fire({
                icon: 'success',
                title: 'Login Successful',
                text: 'You have successfully logged in!',
            }).then(() => {
                // Redirect to home page
                window.location.href = '/Home/Index';
            });
        })
        .catch((error) => {
            Swal.close();
            Swal.fire({
                icon: 'error',
                title: 'Login Failed',
                text: error.message || 'An error occurred during the login process. Please try again.',
            });
        });
}
