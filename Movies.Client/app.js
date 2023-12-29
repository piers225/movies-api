async function searchMovies() {
    const formData = {
        Title: encodeURIComponent(document.getElementById('title').value),
        Genre: encodeURIComponent(document.getElementById('genre').value),
        Limit: encodeURIComponent(document.getElementById('limit').value),
        Page: encodeURIComponent(document.getElementById('page').value),
        SortField: encodeURIComponent(document.getElementById('sortField').value),
        SortDirection: encodeURIComponent(document.getElementById('sortDirection').value)
    };

    const queryString = Object.entries(formData)
        .filter(([_, value]) => value !== '')
        .map(([key, value]) => `${key}=${value}`)
        .join('&');

    const apiUrl = `api/movies?${queryString}`;

    try {
        const response = await fetch(apiUrl);

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const data = await response.json();
        displayResults(data);
    } catch (error) {
        console.error('Error:', error);
    }
}

function displayResults(results) {
    const resultsDiv = document.getElementById('results');
    resultsDiv.innerHTML = '';

    results.forEach(movie => {
        const movieDiv = document.createElement('div');
        movieDiv.innerHTML = `<strong>Title:</strong> ${movie.title}, <strong>Release Date:</strong> ${new Date(movie.releaseDate).toLocaleDateString()}`;
        resultsDiv.appendChild(movieDiv);
    });
}
