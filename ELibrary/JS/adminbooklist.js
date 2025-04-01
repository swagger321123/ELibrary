//document.addEventListener("DOMContentLoaded", function () {
//    const images = document.querySelectorAll(".card-img-top");
//    images.forEach(img => {
//        img.onerror = function () {
//            this.src = "../Images/default.jpg"; // Default image path
//        };
//    });
//});

document.addEventListener("DOMContentLoaded", function () {
    const images = document.querySelectorAll(".card-img-top");
    images.forEach(img => {
        img.onerror = function () {
            this.src = "../ELibrary/default.jpg"; // Default image path
        };
    });
});

 //Handle image errors (fallback to default image)
document.querySelectorAll(".card-img-top").forEach(img => {
    img.onerror = function () {
        this.src = "../ELibrary/default.jpg";
    };
});