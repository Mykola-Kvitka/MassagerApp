function showHideReplyForm(sender) {
    var child = sender.nextSibling.nextSibling
    if (child.style.display == 'initial') {
        child.style.display = 'none'
    } else {
        child.style.display = 'initial'
    }

}
