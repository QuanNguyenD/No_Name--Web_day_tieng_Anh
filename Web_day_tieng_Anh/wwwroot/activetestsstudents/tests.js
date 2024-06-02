document.addEventListener("DOMContentLoaded", function () {
    const radios = document.querySelectorAll('.game-options-container input[type="radio"]');
    radios.forEach((radio) => {
        radio.addEventListener("change", checkForAnswer);
    });

    // Hiển thị câu hỏi đầu tiên
    document.querySelector('.game-question-container').style.display = 'block';

    let currentQuestionIndex = 0;
    let playerScore = sessionStorage.getItem('playerScore') ? parseInt(sessionStorage.getItem('playerScore')) : 0; // Biến lưu trữ điểm của người chơi
    let wrongAnswers = sessionStorage.getItem('wrongAnswers') ? parseInt(sessionStorage.getItem('wrongAnswers')) : 0; // Biến lưu trữ số câu trả lời sai
    const questions = document.querySelectorAll('.game-question-container');
    const totalQuestions = questions.length;

    document.getElementById('player-score').innerText = playerScore;
    document.getElementById('total-questions').innerText = totalQuestions; // Cập nhật số tổng câu hỏi

    function handleNextQuestion() {
        // Ẩn câu hỏi hiện tại
        questions[currentQuestionIndex].style.display = 'none';

        // Tăng chỉ số câu hỏi hiện tại
        currentQuestionIndex++;

        // Hiển thị câu hỏi tiếp theo nếu có
        if (currentQuestionIndex < totalQuestions) {
            questions[currentQuestionIndex].style.display = 'block';
            document.getElementById('question-number').innerText = currentQuestionIndex + 1;
        } else {
            // Hiển thị thông báo hoàn thành nếu đã trả lời hết câu hỏi
            document.getElementById('score-modal').style.display = 'block';
            document.getElementById('right-answers').innerText = playerScore;
            document.getElementById('wrong-answers').innerText = wrongAnswers;
            document.getElementById('grade-percentage').innerText = ((playerScore / totalQuestions) * 100).toFixed(2);
            document.getElementById('remarks').innerText = playerScore === totalQuestions ? "Excellent!" : "Good Try!";
        }

        // Cập nhật điểm số sau khi chuyển sang câu hỏi tiếp theo
        document.getElementById('player-score').innerText = playerScore;
        document.getElementById('total-questions').innerText = totalQuestions;
    }

    window.handleNextQuestion = handleNextQuestion;

    function checkForAnswer(event) {
        const selectedRadio = event.target;
        const questionContainer = selectedRadio.closest('.game-question-container');
        const options = questionContainer.querySelectorAll('.game-options-container span');

        // Kiểm tra nếu câu hỏi đã được trả lời
        if (questionContainer.dataset.answered === "true") return;

        let isCorrect = false;

        options.forEach((option) => {
            const radio = option.querySelector('input[type="radio"]');
            if (radio.dataset.correct === "True") {
                if (radio === selectedRadio) {
                    isCorrect = true;
                }
            }
        });

        // Cập nhật điểm số nếu câu trả lời đúng hoặc sai
        if (isCorrect) {
            playerScore++;
            sessionStorage.setItem('playerScore', playerScore);
        } else {
            wrongAnswers++;
            sessionStorage.setItem('wrongAnswers', wrongAnswers);
        }

        // Đánh dấu câu hỏi là đã được trả lời
        questionContainer.dataset.answered = "true";

        // Hiển thị màu sắc câu trả lời
        options.forEach((option) => {
            const radio = option.querySelector('input[type="radio"]');
            if (radio.dataset.correct === "True") {
                option.style.backgroundColor = "green"; // Đổi màu nền thành màu xanh lá cho đáp án đúng
            } else {
                option.style.backgroundColor = "red"; // Đổi màu nền thành màu đỏ cho các đáp án sai
            }
        });

        // Disable all radio buttons after answer selection
        options.forEach((option) => {
            const radio = option.querySelector('input[type="radio"]');
            radio.disabled = true;
        });

        // Cập nhật điểm số sau khi người dùng chọn câu trả lời
        document.getElementById('player-score').innerText = playerScore;
    }

    window.checkForAnswer = checkForAnswer;
});

function closeScoreModal() {
    // Reset dữ liệu khi đóng modal
    sessionStorage.removeItem('playerScore');
    sessionStorage.removeItem('wrongAnswers');

    document.getElementById('score-modal').style.display = "none";
}

function closeOptionModal() {
    document.getElementById('option-modal').style.display = "none";
}

window.addEventListener('beforeunload', function (event) {
    // Xóa dữ liệu khi rời khỏi trang
    sessionStorage.removeItem('playerScore');
    sessionStorage.removeItem('wrongAnswers');
});
