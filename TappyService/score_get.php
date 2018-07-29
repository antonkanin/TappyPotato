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

	$sql = "select t1.player_name, max(t1.score) as score, t1.death_position from score_board as t1
		inner join score_board as t2
		on t1.number = t2.number
		group by t1.player_id";
	
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