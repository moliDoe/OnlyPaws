const themeToggle = document.getElementById("themeToggle");
const themeIcon = document.getElementById("themeIcon");
const savedTheme = localStorage.getItem("theme") || "light";

document.documentElement.dataset.theme = savedTheme;
if (themeIcon) {
    if(savedTheme === "light") {
        themeIcon.className = "bi bi-moon-stars";
    } 
    else {
        themeIcon.className = "bi bi-sun";
    }
}

if (themeToggle && themeIcon) {
    themeToggle.addEventListener("click", function() {
        if(themeIcon.className === "bi bi-moon-stars"){
            themeIcon.className = "bi bi-sun";
            document.documentElement.dataset.theme = "dark"
            localStorage.setItem("theme", "dark");
        } 
        else if (themeIcon.className === "bi bi-sun"){
            themeIcon.className = "bi bi-moon-stars";
            document.documentElement.dataset.theme = "light"
            localStorage.setItem("theme", "light");
        }
    })
}
