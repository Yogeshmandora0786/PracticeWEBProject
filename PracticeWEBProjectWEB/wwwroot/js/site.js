const signInBtnLink = document.querySelector('.signInBtn-link');
const signUpBtnLink = document.querySelector('.signUpBtn-link');
const wrapper = document.querySelector('.wrapper');
signUpBtnLink.addEventListener('click', () => {
    wrapper.classList.toggle('active');
});
signInBtnLink.addEventListener('click', () => {
    wrapper.classList.toggle('active');
});


// log in Function
//$(document).ready(function () {
//    debugger
//    $('#loginForm').submit(function (event) {
//        event.preventDefault(); // Prevent default form submission

//        // Collect form data
//        var formData = new FormData(this);

//        // Show loader using SweetAlert2
//        Swal.fire({
//            title: 'Logging in...',
//            text: 'Please wait while we log you in.',
//            allowOutsideClick: false,
//            showConfirmButton: false,
//            didOpen: () => {
//                Swal.showLoading();
//            }
//        });

//        // AJAX request
//        $.ajax({
//            url: '' + 'https://localhost:7146/' + 'api/LogInController/LoginActiveInactive',
//            type: 'POST',
//            data: formData,
//            contentType: false,
//            processData: false,
//            success: function (response) {
//                setTimeout(() => { // Minimum delay for loader
//                    Swal.close(); // Close loader
//                    if (response.data.code === 200) {
//                        console.log("Redirecting to /Home/Index");
//                        window.location.href = '/Home/Index'; // Redirect to home page
//                    } else {
//                        console.error("Invalid Login:", response);
//                        Swal.fire({
//                            icon: 'error',
//                            title: 'Login Failed',
//                            text: response.data.message || 'Invalid username or password.',
//                        });
//                    }
//                }, 1000); // Loader displays for at least 1 second
//            },
//            error: function (xhr, status, error) {
//                setTimeout(() => {
//                    Swal.close(); // Close loader
//                    console.error("AJAX Error:", status, error);
//                    Swal.fire({
//                        icon: 'error',
//                        title: 'Error',
//                        text: 'An error occurred during the login process. Please try again.',
//                    });
//                }, 1000);
//            }
//        });
//    });
//});
//document.getElementById('loginForm').addEventListener('submit', function (event) {
//    event.preventDefault(); // Prevent the default form submission

//    // Collect form data
//    const username = document.getElementById('username').value;
//    const password = document.getElementById('password').value;

//    // AJAX call using Fetch API
//    fetch('@Url.Action("Login", "Registration")', {
//        method: 'POST',
//        headers: {
//            'Content-Type': 'application/json',
//            'RequestVerificationToken': '@Html.AntiForgeryToken()' // Include Anti-Forgery Token for security
//        },
//        body: JSON.stringify({ username, password })
//    })
//        .then(response => response.json())
//        .then(data => {
//            const responseMessage = document.getElementById('responseMessage');
//            if (data.success) {
//                responseMessage.style.color = 'green';
//                responseMessage.textContent = 'Login successful!';
//                // Redirect to dashboard or another page
//                window.location.href = '@Url.Action("Dashboard", "Home")';
//            } else {
//                responseMessage.style.color = 'red';
//                responseMessage.textContent = data.message || 'Invalid username or password.';
//            }
//        })
//        .catch(error => {
//            const responseMessage = document.getElementById('responseMessage');
//            responseMessage.style.color = 'red';
//            responseMessage.textContent = 'An error occurred. Please try again.';
//            console.error('Error:', error);
//        });
//});




// Add event listener to handle the form submission
document.getElementById('signUpForm').addEventListener('submit', function (event) {
    event.preventDefault(); // Prevent the default form submission

    // Collect form data
    const username = document.getElementById('username').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    // Validation for password confirmation
    if (password !== confirmPassword) {
        const responseMessage = document.getElementById('responseMessage');
        responseMessage.style.color = 'red';
        responseMessage.textContent = 'Passwords do not match.';
        return;
    }

    // AJAX call using Fetch API
    fetch('@Url.Action("Index", "Registration")', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': '@Html.AntiForgeryToken()' // Include Anti-Forgery Token for security
        },
        body: JSON.stringify({ username, email, password })
    })
        .then(response => response.json())
        .then(data => {
            const responseMessage = document.getElementById('responseMessage');
            if (data.success) {
                responseMessage.style.color = 'green';
                responseMessage.textContent = 'Sign-up successful! Redirecting...';
                // Redirect to login or another page
                setTimeout(() => {
                    window.location.href = '@Url.Action("Login", "Registration")';
                }, 2000);
            } else {
                responseMessage.style.color = 'red';
                responseMessage.textContent = data.message || 'Sign-up failed. Please try again.';
            }
        })
        .catch(error => {
            const responseMessage = document.getElementById('responseMessage');
            responseMessage.style.color = 'red';
            responseMessage.textContent = 'An error occurred. Please try again.';
            console.error('Error:', error);
        });
});
