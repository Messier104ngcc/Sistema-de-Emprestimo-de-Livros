﻿
$(document).ready(function () {
        
    // Scrip para o desaparecimento das mensagem de erro, sucesso e lert.
    setTimeout(function () {
        $(".alert").fadeOut("slow", function () {
            $(this).alert('close');
        })
    }, 5000)
})





// Scrip para foto da capa aparecer no campo de seleção de arquivos no formulario de cadastro de livros.
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



// Scrip dos campos do formulario de cadastro de livros para verificar se foi preenchidos ou não.
(() => {
    'use strict'
  
    const forms = document.querySelectorAll('.needs-validation')

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



// Scrip para truncar os textos das descrições dos livros na tabela

function toggleText(event, id) {
    const curta = document.getElementById(`descricaoCurta-${id}`);
    const completa = document.getElementById(`descricaoCompleta-${id}`);
    const btn = event.target;

    if (curta.classList.contains('d-none')) {
        curta.classList.remove('d-none');
        completa.classList.add('d-none');
        btn.textContent = 'Ver mais';
    } else {
        curta.classList.add('d-none');
        completa.classList.remove('d-none');
        btn.textContent = 'Ver menos';
    }
}



// tooltip icones

document.addEventListener('DOMContentLoaded', function () {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
});


