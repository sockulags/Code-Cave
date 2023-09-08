// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// selectors
const themeToggleBtn = document.querySelector('.darkmode-input');

// state
const theme = localStorage.getItem('theme');

// on mount
theme && document.body.classList.add(theme);

// handlers
const handleThemeToggle = () => {
    document.body.classList.toggle('dark-mode');
    if (document.body.classList.contains('dark-mode')) {
        localStorage.setItem('theme', 'dark-mode');
    } else {
        localStorage.removeItem('theme');
    }
    
    var style = document.getElementById('style');
    if (style.getAttribute('href') === '/css/prism.css') {
        style.setAttribute('href', '/css/prism-darkmode.css');
    } else {
        style.setAttribute('href', '/css/prism.css');
    }
     

};

// events
themeToggleBtn.addEventListener('click', handleThemeToggle);

// change stylesheet using JavaScript
