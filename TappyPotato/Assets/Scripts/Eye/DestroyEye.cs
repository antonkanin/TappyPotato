public class DestroyEye : BaseTappyController
{
    protected override void OnGameOverConfirmed()
    {
        Destroy(gameObject);
    }
}