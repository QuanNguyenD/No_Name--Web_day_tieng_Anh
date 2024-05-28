<script>
    document.addEventListener("DOMContentLoaded", function () {
        const form = document.querySelector("form[asp-action='Add']");

    if (form) {
        form.addEventListener("submit", function (event) {
            event.preventDefault(); // Ngăn chặn form submit mặc định
            addQuestion(); // Gọi hàm để thêm câu hỏi
        });
        }

    // Hàm thêm câu hỏi
    function addQuestion() {
            const questionContent = document.getElementById("QuestionContent").value;
    const answers = [];

    // Lặp qua các câu trả lời và thu thập dữ liệu từ Model hoặc trường nhập liệu
    for (let i = 0; i < 4; i++) {
                const answerContent = document.getElementById(`Answers_${i}__AnswerContent`).value;
    const isCorrect = document.getElementById(`Answers_${i}__IsCorrect`).checked;

    answers.push({
        answerContent: answerContent,
    isCorrect: isCorrect
                });
            }

    // Gửi dữ liệu đến server
    sendDataToServer(questionContent, answers);
        }

    function sendDataToServer(questionContent, answers) {
            const xhr = new XMLHttpRequest();
    xhr.open("POST", form.action, true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
                if (xhr.readyState === 4 && xhr.status === 200) {
        // Xử lý phản hồi từ server nếu cần
        console.log("Data saved successfully!");
                }
            };
    const data = JSON.stringify({questionContent: questionContent, answers: answers });
    xhr.send(data);
        }

        let shuffledQuestions = [];

        function handleQuestions() {
            while (shuffledQuestions.length <= 9) {
                const random = questions[Math.floor(Math.random() * questions.length)];
                if (!shuffledQuestions.includes(random)) {
                    shuffledQuestions.push(random);
                }
            }
        }

        let questionNumber = 1;
        let playerScore = 0;
        let wrongAttempt = 0;
        let indexNumber = 0;


    function NextQuestion(index) {
        handleQuestions();
    const currentQuestion = shuffledQuestions[index];
    document.getElementById("question-number").innerHTML = questionNumber;
    document.getElementById("player-score").innerHTML = playerScore;
    document.getElementById("display-question").innerHTML = currentQuestion.question;

    // Lấy ra tất cả các radio button trong class "radio"
    const radioButtons = document.querySelectorAll('.radio');

    // Lặp qua từng radio button và gán giá trị từ mảng "Answers" vào thuộc tính "value"
    radioButtons.forEach((radio, i) => {
        radio.value = currentQuestion["option" + String.fromCharCode(65 + i)];
    document.getElementById("option-" + (i + 1) + "-label").innerHTML = radio.value;
    }
}




        function checkForAnswer() {
            const currentQuestion = shuffledQuestions[indexNumber];
            const currentQuestionAnswer = currentQuestion.correctOption;
            const options = document.getElementsByName("option");
            let correctOption = null;

            options.forEach((option) => {
                if (option.value === currentQuestionAnswer) {
                    correctOption = option.labels[0].id;
                }
            });

            if (options[0].checked === false && options[1].checked === false && options[2].checked === false && options[3].checked === false) {
                document.getElementById('option-modal').style.display = "flex";
            }

            options.forEach((option) => {
                if (option.checked === true && option.value === currentQuestionAnswer) {
                    document.getElementById(correctOption).style.backgroundColor = "green";
                    playerScore++;
                    indexNumber++;
                    setTimeout(() => {
                        questionNumber++;
                    }, 1000);
                } else if (option.checked && option.value !== currentQuestionAnswer) {
                    const wrongLabelId = option.labels[0].id;
                    document.getElementById(wrongLabelId).style.backgroundColor = "red";
                    document.getElementById(correctOption).style.backgroundColor = "green";
                    wrongAttempt++;
                    indexNumber++;
                    setTimeout(() => {
                        questionNumber++;
                    }, 1000);
                }
            });
        }

        function handleNextQuestion() {
            checkForAnswer();
            unCheckRadioButtons();
            setTimeout(() => {
                if (indexNumber <= 9) {
                    NextQuestion(indexNumber);
                } else {
                    handleEndGame();
                }
                resetOptionBackground();
            }, 1000);
        }

        function resetOptionBackground() {
            const options = document.getElementsByName("option");
            options.forEach((option) => {
                document.getElementById(option.labels[0].id).style.backgroundColor = "";
            });
        }

        function unCheckRadioButtons() {
            const options = document.getElementsByName("option");
            for (let i = 0; i < options.length; i++) {
                options[i].checked = false;
            }
        }

        function handleEndGame() {
            let remark = null;
            let remarkColor = null;

            if (playerScore <= 3) {
                remark = "Bad Grades, Keep Practicing.";
                remarkColor = "red";
            } else if (playerScore >= 4 && playerScore < 7) {
                remark = "Average Grades, You can do better.";
                remarkColor = "orange";
            } else if (playerScore >= 7) {
                remark = "Excellent, Keep the good work going.";
                remarkColor = "green";
            }
            const playerGrade = (playerScore / 10) * 100;

            document.getElementById('remarks').innerHTML = remark;
            document.getElementById('remarks').style.color = remarkColor;
            document.getElementById('grade-percentage').innerHTML = playerGrade;
            document.getElementById('wrong-answers').innerHTML = wrongAttempt;
            document.getElementById('right-answers').innerHTML = playerScore;
            document.getElementById('score-modal').style.display = "flex";
        }

        function closeScoreModal() {
            questionNumber = 1;
            playerScore = 0;
            wrongAttempt = 0;
            indexNumber = 0;
            shuffledQuestions = [];
            NextQuestion(indexNumber);
            document.getElementById('score-modal').style.display = "none";
        }

        function closeOptionModal() {
            document.getElementById('option-modal').style.display = "none";
        }

  });
    </script>