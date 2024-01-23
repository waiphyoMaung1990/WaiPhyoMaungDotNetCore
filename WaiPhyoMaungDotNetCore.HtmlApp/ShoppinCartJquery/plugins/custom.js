 // Function to show SweetAlert notification
 const showNotification = (title, message, icon, timer) => {
    Swal.fire({
        title: title,
        text: message,
        icon: icon,
        timer: timer,
        showConfirmButton: false,
    });
};

const showToast = (message, type) => {
    Toastify({
        text: message,
        duration: 3000, // 3 seconds
        gravity: "top", // top or bottom
        position: "right", // right, left, or center
        backgroundColor: type === "success" ? "#28a745" : "#dc3545", // Green for success, red for error
    }).showToast();
};
