// sweetAlert
// function successMessage(message) {
//     Swal.fire({
//         title: "Success",
//         text: message,
//         icon: "success"
//     });
// }
// notiiflix
function successMessage(message) {
    Notiflix.Report.success(
        'Notiflix Success',
        message,
        'Okay',
    );
}

///sweet
function confirmMessage(message) {
    //promis->then
    return new Promise(function (myResolve, myReject) {
        Swal.fire({
            title: "Confirm",
            text: "Are you sure Want to delete?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes"
        }).then((result) => {
            //result ထဲမှာဘဲtrue false ပြန်
            myResolve(result.isConfirmed);
        });
    });
    return new Promise(function (myResolve, myReject) {
        const result = confirm(message);
        myResolve(result);
    });


//notiflix
// function confirmMessage(message) {

//     return new Promise(function (myResolve, myReject) {
//         Notiflix.Confirm.show(
//             ' Confirm',
//             message,
//             'Yes',
//             'No',
//             function okCb() {
//                 myResolve(true);
//             },
//             function cancelCb() {
//                 myResolve(false);
//             }

//         );

//     });
// }
    // "Consuming Code" (Must wait for a fulfilled Promise)
    myPromise.then(
        function (value) { /* code if successful */ },
        function (error) { /* code if some error */ }
    );


}
