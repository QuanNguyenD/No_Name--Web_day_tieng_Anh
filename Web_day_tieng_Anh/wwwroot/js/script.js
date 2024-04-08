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

let section = document.querySelectorAll('section');
let navLinks = document.querySelectorAll('.flex-div .nav-right a')

window.onscroll = () => {
    section.forEach(sec => {
        let top = window.scrollY;
        let offset = sec.offsetTop - 150;
        let height = sec.offsetHeight;
        let id = sec.getAttribute('id');


        if (top >= offset && top < offset + height) {
            navLinks.forEach(links => {
                links.classList.remove('check-active');
                document.querySelector('.flex-div .nav-right a[href*=' + id + ']').classList.add('check-active');
            });
        };
    });
};

