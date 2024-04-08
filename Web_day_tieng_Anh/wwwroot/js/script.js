const icon = document.querySelector('.icon');
const search = document.querySelector('.search');
icon.onclick = function () {
    search.classList.toggle('active');
}

let menu = document.querySelector('.menu-icon');
let navright = document.querySelector('.nav-right');

menu.onclick = () => {
    navright.classList.toggle('active');

}



