<?php
	$con = mysqli_connect('188.121.44.160', 'fmpscores', 'Password1!', 'fmpscores');

	// check for successful connection
	if (mysqli_connect_errno()){
		echo "1: Connection failed";
		exit();
	}
	
	$perPage = $_POST["perPage"];
	$pageNum = $_POST["pageNum"];
	$offset = ($pageNum - 1) * $perPage;
	
	// get leaderboard info
	$query = "SELECT id, name, avatar, colour, score, FIND_IN_SET(score, (SELECT GROUP_CONCAT(score ORDER BY score DESC) FROM players LEFT JOIN scores ON players.id = scores.user_id)) AS rank FROM players LEFT JOIN scores ON players.id = scores.user_id ORDER BY rank ASC LIMIT $perPage OFFSET $offset";
	$result = mysqli_query($con, $query) or die("3: Failed to load leaderboard");
    if (mysqli_num_rows($result) > 0){
        while($row = mysqli_fetch_assoc($result)){
            echo 'id:' . $row["id"] . '|name:' . $row["name"] . '|avatar:' . $row["avatar"] . '|colour:' . $row["colour"] . '|score:' . $row["score"] . '|rank:' . $row["rank"] . ";";
        }
    }
    else{    
        echo "2: Leaderboard not found";
    }
?>