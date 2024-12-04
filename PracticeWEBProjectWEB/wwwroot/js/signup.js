function Register(event) {
    event.preventDefault(); // Prevent form submission

    // Get user input
    const username = document.getElementById('username').value.trim();
    const email = document.getElementById('email').value.trim();
    const password = document.getElementById('password').value.trim();
    const confirmPassword = document.getElementById('confirmPassword').value.trim();

    // Validate inputs
    if (!username || !email || !password || !confirmPassword) {
        Swal.fire({
            icon: 'warning',
            title: 'Missing Fields',
            text: 'Please fill in all fields!',
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

    // Create FormData object to send the registration data
    const formData = new FormData();
    formData.append('Username', username);
    formData.append('Email', email);
    formData.append('Password', password);

    // Show loading alert
    Swal.fire({
        title: 'Registering...',
        text: 'Please wait while we process your registration.',
        allowOutsideClick: false,
        showConfirmButton: false,
        didOpen: () => {
            Swal.showLoading();
        },
    });

    // Make the AJAX request
    $.ajax({
        url: '/Registration/RegisterPost',  
        type: 'POST',
        data: formData,
        contentType: false,  
        processData: false, 
        success: function (response) {
            setTimeout(() => { 
                Swal.close(); 
                if (response.responseCode === 200) {
                    console.log("Registration successful. Redirecting to login page.");
                    window.location.href = '/Login/Login'; 
                } else {
                    console.error("Registration Failed:", response);
                    Swal.fire({
                        icon: 'error',
                        title: 'Registration Failed',
                        text: response.responseMessage, 
                    });
                }
            }, 1000); 
        },
        error: function (xhr, status, error) {
            setTimeout(() => {
                Swal.close(); 
                console.error("AJAX Error:", status, error);
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'An error occurred during the registration process.',
                });
            }, 1000);
        }
    });
}
