function showHideReplyForm(sender) {
    var child = sender.nextSibling.nextSibling
    if (child.style.display == 'initial') {
        child.style.display = 'none'
    } else {
        child.style.display = 'initial'
    }

}

function showHideNewCommentForm(id) {
    var element = document.getElementById(id)
    if (element.style.display == 'initial') {
        element.style.display = 'none'
    } else {
        element.style.display = 'initial'
    }
}
