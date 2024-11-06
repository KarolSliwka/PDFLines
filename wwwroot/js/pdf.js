$(document).ready(function () {
    window.onload = function () {
        var fiveMinutes = 60 * 5,
            display = document.querySelector('#refresh-at');
        startTimer(fiveMinutes, display);
    };
});

function startTimer(duration, display) {
    var timer = duration, minutes, seconds;
    setInterval(function () {
        minutes = parseInt(timer / 60, 10);
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = minutes + ":" + seconds;

        if (--timer < 0) {
            timer = duration;
            window.location.reload();
        }
    }, 1000);
}