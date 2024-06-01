function handleNextQuestion() {
    checkForAnswer();
}

function checkForAnswer() {
    const options = document.querySelectorAll('.game-options-container span');
    options.forEach((option) => {
        const radio = option.querySelector('input[type="radio"]');
        if (radio.dataset.correct === "True") {
            option.style.backgroundColor = "green"; // Đổi màu nền thành màu xanh lá cho đáp án đúng
        } else {
            option.style.backgroundColor = "red"; // Đổi màu nền thành màu đỏ cho các đáp án sai
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    const checkQuestionButton = document.querySelector('.next-button-container button');
    checkQuestionButton.onclick = handleNextQuestion;
});

function closeScoreModal() {
    document.getElementById('score-modal').style.display = "none";
}

function closeOptionModal() {
    document.getElementById('option-modal').style.display = "none";
}