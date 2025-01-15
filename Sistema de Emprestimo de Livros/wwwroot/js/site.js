//const inputFile = document.querySelector("#Imagem");

//inputFile.addEventListener("change", function (e) {
//    const inputTarget = e.target;
//    const file = inputTarget.files[0];

//    if (file) {
//        const reader = new FileReader();

//        reader.addEventListener("load", function (e) {
//            const readerTarget = e.target;
//            const img = document.querySelector("#img");
//            img.src = readerTarget.result;

//            // const figcaption = document.querySelector("#figcaption");
//            // figcaption.innerHTML = file.name;
//        });

//        reader.readAsDataURL(file);
//    }
//});

document.addEventListener('DOMContentLoaded', function () {
const inputFile = document.querySelector("#Imagem");
    if (inputFile) {
inputFile.addEventListener("change", function (e) {
    const inputTarget = e.target;
    const file = inputTarget.files[0];

    if (file) {
        const reader = new FileReader();

        reader.addEventListener("load", function (e) {
            const readerTarget = e.target;
            const img = document.querySelector("#img");
            img.src = readerTarget.result;

            // const figcaption = document.querySelector("#figcaption");
            // figcaption.innerHTML = file.name;
        });

        reader.readAsDataURL(file);
    }
});
    } else {
        console.error("Elemento não encontrado: #Imagem");
    }
});



// Example starter JavaScript for disabling form submissions if there are invalid fields
(() => {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    const forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.from(forms).forEach(form => {
        form.addEventListener('submit', event => {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }

            form.classList.add('was-validated')
        }, false)
    })
})()


