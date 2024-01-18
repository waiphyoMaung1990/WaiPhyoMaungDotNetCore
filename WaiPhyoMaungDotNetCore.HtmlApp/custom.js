function successMessage(message) {
    Swal.fire({
        title: "Success",
        text: message,
        icon: "success"
    });
}

function confirmMessage(message) {
    //promis->then
    // return new Promise(function (myResolve, myReject) {
    //     Swal.fire({
    //         title: "Confirm",
    //         text: "Are you sure Want to delete?",
    //         icon: "warning",
    //         showCancelButton: true,
    //         confirmButtonText: "Yes"
    //     }).then((result) => {
    //         //result ထဲမှာဘဲtrue false ပြန်
    //         myResolve(result.isConfirmed);
    //     });
    // });
    return new Promise(function (myResolve, myReject) {
        const result = confirm(message);
        myResolve(result);
    });
}
