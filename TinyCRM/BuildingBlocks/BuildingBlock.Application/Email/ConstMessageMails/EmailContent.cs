namespace BuildingBlock.Application.Email.ConstMessageMails;

public static class EmailContent
{
    public static readonly string RabbitMqConnectionIssueContent = "Dear recipient,\n\n" +
                                                                   "We would like to inform you that there is an issue with the RabbitMQ connection. " +
                                                                   "Our system has detected that the connection to RabbitMQ is currently on shutdown. " +
                                                                   "This may impact the functionality of our application.\n\n" +
                                                                   "Thank you for your attention.\n\n" +
                                                                   "Sincerely,\n" +
                                                                   "Tuan Nguyen Vip Pro";
}