document.addEventListener("DOMContentLoaded", function () {
    const radios = document.querySelectorAll('.game-options-container input[type="radio"]');
    radios.forEach((radio) => {
        radio.addEventListener("change", checkForAnswer);
    });

    // Hiển thị câu hỏi đầu tiên
    document.querySelector('.game-question-container').style.display = 'block';

    let currentQuestionIndex = 0;
    let playerScore = sessionStorage.getItem('playerScore') ? parseInt(sessionStorage.getItem('playerScore')) : 0;
    let wrongAnswers = sessionStorage.getItem('wrongAnswers') ? parseInt(sessionStorage.getItem('wrongAnswers')) : 0;
    const questions = document.querySelectorAll('.game-question-container');
    const totalQuestions = questions.length;

    document.getElementById('player-score').innerText = playerScore;
    document.getElementById('total-questions').innerText = totalQuestions;

    function handleNextQuestion() {
        questions[currentQuestionIndex].style.display = 'none';
        currentQuestionIndex++;

        if (currentQuestionIndex < totalQuestions) {
            questions[currentQuestionIndex].style.display = 'block';
            document.getElementById('question-number').innerText = currentQuestionIndex + 1;
        } else {
            document.getElementById('score-modal').style.display = 'block';
            document.getElementById('right-answers').innerText = playerScore;
            document.getElementById('wrong-answers').innerText = wrongAnswers;
            document.getElementById('grade-percentage').innerText = ((playerScore / totalQuestions) * 100).toFixed(2);
            document.getElementById('remarks').innerText = playerScore === totalQuestions ? "Excellent!" : "Good Try!";
            saveUserScores();
        }

        document.getElementById('player-score').innerText = playerScore;
        document.getElementById('total-questions').innerText = totalQuestions;
    }

    window.handleNextQuestion = handleNextQuestion;

    function checkForAnswer(event) {
        const selectedRadio = event.target;
        const questionContainer = selectedRadio.closest('.game-question-container');
        const options = questionContainer.querySelectorAll('.game-options-container span');

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

        if (isCorrect) {
            playerScore++;
            sessionStorage.setItem('playerScore', playerScore);
        } else {
            wrongAnswers++;
            sessionStorage.setItem('wrongAnswers', wrongAnswers);
        }

        questionContainer.dataset.answered = "true";

        options.forEach((option) => {
            const radio = option.querySelector('input[type="radio"]');
            if (radio.dataset.correct === "True") {
                option.style.backgroundColor = "green";
            } else {
                option.style.backgroundColor = "red";
            }
        });

        options.forEach((option) => {
            const radio = option.querySelector('input[type="radio"]');
            radio.disabled = true;
        });

        document.getElementById('player-score').innerText = playerScore;

        userScores.push({
            userId: userId,
            fullName: fullName,
            testId: selectedRadio.dataset.testId,
            questionId: selectedRadio.dataset.questionId,
            questionContent: selectedRadio.dataset.questionContent,
            answerContent: selectedRadio.dataset.answerContent,
            isCorrect: isCorrect,
            points: isCorrect ? 1 : 0
        });
    }

    window.checkForAnswer = checkForAnswer;

    function closeScoreModal() {
        sessionStorage.removeItem('playerScore');
        sessionStorage.removeItem('wrongAnswers');

        document.getElementById('score-modal').style.display = "none";
    }

    window.closeScoreModal = closeScoreModal;

    function closeOptionModal() {
        document.getElementById('option-modal').style.display = "none";
    }

    window.addEventListener('beforeunload', function (event) {
        sessionStorage.removeItem('playerScore');
        sessionStorage.removeItem('wrongAnswers');
    });
});