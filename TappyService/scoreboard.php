<html>

<table>
    <tr>
        <td>Name</td>
        <td>Score</td>
    </tr>
<?php
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
    
    while ($row = $result->fetch_assoc()) {
        echo $row["player_name"];
        echo $row["score"];
    }
?>
</table>

</html>