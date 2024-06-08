document.addEventListener('DOMContentLoaded', function () {
    var likeButton = document.getElementById('likeButton');
    var likeCountSpan = document.getElementById('likeCount');
    var likeIcon = document.getElementById('likeIcon');

    // Khởi tạo số like từ Local Storage hoặc mặc định là 0 nếu không có
    var likeCount = parseInt(localStorage.getItem('likeCount')) || 0;
    likeCountSpan.textContent = likeCount + " likes";

    // Biến kiểm tra xem người dùng đã like chưa
    var liked = localStorage.getItem('liked') === 'true';

    // Cập nhật trạng thái của nút like và icon
    if (liked) {
        likeButton.classList.add('liked');
        likeIcon.classList.add('liked');
    }

    likeButton.addEventListener('click', function () {
        if (!liked) {
            likeCount++;
            localStorage.setItem('likeCount', likeCount);
            likeCountSpan.textContent = likeCount + " likes";
            localStorage.setItem('liked', 'true');
            likeButton.classList.add('liked');
            likeIcon.classList.add('liked');
            liked = true;
        } else {
            likeCount--;
            localStorage.setItem('likeCount', likeCount);
            likeCountSpan.textContent = likeCount + " likes";
            localStorage.setItem('liked', 'false');
            likeButton.classList.remove('liked');
            likeIcon.classList.remove('liked');
            liked = false;
        }
    });
});

    document.addEventListener('DOMContentLoaded', function () {
        var addCommentForm = document.querySelector('.add-comment');
        var commentBoxContainer = document.getElementById('commentBoxContainer');
        var commentCountHeading = document.getElementById('commentCountHeading');
        var editMode = false;
        var editedCommentIndex = null;

        var lessonId = document.getElementById('lessonId').value;
        var currentUser = document.getElementById('userName').value; // Lấy username của người dùng hiện tại
        var comments = JSON.parse(localStorage.getItem('comments_' + lessonId)) || [];

        commentCountHeading.textContent = comments.length + " Comments";

        comments.forEach(function (comment, index) {
            var commentBox = createCommentBox(comment, index, currentUser);
            commentBoxContainer.appendChild(commentBox);
        });

        addCommentForm.addEventListener('submit', function (event) {
            event.preventDefault();

            var commentText = document.getElementById('commentText').value;
            var userName = document.getElementById('userName').value;

            if (commentText.trim() === '') {
                alert('Please enter your comment.');
                return;
            }

            if (editMode) {
                comments[editedCommentIndex].text = commentText;
                localStorage.setItem('comments_' + lessonId, JSON.stringify(comments));
                refreshComments(currentUser);
                editMode = false;
                editedCommentIndex = null;
            } else {
                var currentDate = new Date();
                var formattedDate = currentDate.getDate() + "-" + (currentDate.getMonth() + 1) + "-" + currentDate.getFullYear() + " " + currentDate.getHours() + ":" + currentDate.getMinutes() + ":" + currentDate.getSeconds();

                var newComment = {
                    text: commentText,
                    date: formattedDate,
                    userName: userName
                };

                comments.push(newComment);
                localStorage.setItem('comments_' + lessonId, JSON.stringify(comments));

                var commentBox = createCommentBox(newComment, comments.length - 1, currentUser);
                commentBoxContainer.appendChild(commentBox);

                commentCountHeading.textContent = comments.length + " Comments";
                document.getElementById('commentText').value = '';
            }
        });

        function createCommentBox(comment, index, currentUser) {
            var commentBox = document.createElement('div');
            commentBox.classList.add('box');
            commentBox.innerHTML = `
                    <div class="user">
                        <img src="~/images/pic-1.jpg" alt="">
                        <div>
                            <h3>${comment.userName}</h3>
                            <span>${comment.date}</span>
                        </div>
                    </div>
                    <div class="comment-box">${comment.text}</div>
                    `;
            // Chỉ hiển thị các nút nếu người dùng hiện tại là chủ sở hữu của comment
            if (comment.userName === currentUser) {
                commentBox.innerHTML += `
                        <form action="" class="flex-btn">
                            <input type="button" value="Edit Comment" name="edit_comment" class="edit-comment-button inline-option-btn" onclick="editComment(${index})">
                            <input type="button" value="Delete Comment" name="delete_comment" class="delete-comment-button inline-delete-btn" onclick="deleteComment(${index})">
                        </form>
                        `;
            }
            return commentBox;
        }

        window.editComment = function (index) {
            var commentText = comments[index].text;
            document.getElementById('commentText').value = commentText;
            editMode = true;
            editedCommentIndex = index;
        }

        window.deleteComment = function (index) {
            comments.splice(index, 1);
            localStorage.setItem('comments_' + lessonId, JSON.stringify(comments));
            refreshComments(currentUser);
            commentCountHeading.textContent = comments.length + " Comments";
        }

        function refreshComments(currentUser) {
            commentBoxContainer.innerHTML = '';
            comments.forEach(function (comment, index) {
                var commentBox = createCommentBox(comment, index, currentUser);
                commentBoxContainer.appendChild(commentBox);
            });
        }
    });