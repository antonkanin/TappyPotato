<?php
    header("Cache-Control: no-cache, no-store, must-revalidate"); // HTTP 1.1.
    header("Pragma: no-cache"); // HTTP 1.0.
    header("Expires: 0"); // Proxies.
        
    $db_settings = parse_ini_file("../private/config.ini");

	$conn = new mysqli($db_settings['servername'], $db_settings['username'], $db_settings['password'], $db_settings['db_name']);

	// Check connection
	if($conn->connect_error) {
		die("ERROR: Could not connect. " . $conn->connect_error);
	}

    //$sql = "select t1.number, t1.player_name, t1.score, t1.death_position from score_board as t1
    //        where t1.score = (select max(t2.score) from score_board t2 where t2.player_id = t1.player_id)";

    $sql = "select CONCAT(player_name, '(', score, ')') as player_name, score, death_position from score_board";

    $result = $conn->query($sql);

	if (!$result) {
		trigger_error("Invalid query: " . $conn->error);
	}

	$resultArray = array();

	if ($result->num_rows > 0) {
		while($row = $result->fetch_assoc()) {
			$resultArray[] = $row;
		}
		echo json_encode($resultArray);
	}
	else {
		echo "no records in the database";
	}

	$conn->close();
?>