const spans = document.querySelectorAll('.approve-status');

spans.forEach(span => {
    const text = span.textContent.trim();

    if (text === 'APPROVE') {
        span.style.color = 'green';
    } else if (text === 'NOT APPROVE') {
        span.style.color = 'red';
    } else if (text === 'PENDING') {
        span.style.color = 'orange';
    }

});