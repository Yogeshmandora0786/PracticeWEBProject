function SignUp(event) {
    event.preventDefault(); // Prevent default form submission

    // Get form values
    const username = document.getElementById('username').value.trim();
    const email = document.getElementById('email').value.trim();
    const password = document.getElementById('password').value.trim();
    const confirmPassword = document.getElementById('confirmPassword').value.trim();

    // Validate input fields
    if (!username || !email || !password || !confirmPassword) {
        Swal.fire({
            icon: 'warning',
            title: 'Missing Fields',
            text: 'All fields are required!',
        });
        return;
    }

    if (password !== confirmPassword) {
        Swal.fire({
            icon: 'warning',
            title: 'Password Mismatch',
            text: 'Passwords do not match!',
        });
        return;
    }

    // Prepare payload
    const payload = {
        Username: username,
        Email: email,
        Password: password,
        ConfirmPassword: confirmPassword,
    };

    // Show loading alert
    Swal.fire({
        title: 'Signing Up...',
        text: 'Please wait while we create your account.',
        allowOutsideClick: false,
        showConfirmButton: false,
        didOpen: () => {
            Swal.showLoading();
        },
    });

    // Make the API call using fetch
    fetch('https://localhost:7146/api/RegistrationApi/Registration_Upsert', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(payload),
    })
        .then(async (response) => {
            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Failed to register.');
            }
            return response.json();
        })
        .then((data) => {
            Swal.close();
            Swal.fire({
                icon: 'success',
                title: 'Sign Up Successful',
                text: 'Your account has been created successfully!',
            }).then(() => {
                // Redirect to login page
                window.location.href = '/Registration/Login';
            });
        })
        .catch((error) => {
            Swal.close();
            Swal.fire({
                icon: 'error',
                title: 'Sign Up Failed',
                text: error.message || 'An error occurred during the sign-up process. Please try again.',
            });
        });
}
