﻿using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using Shouldly;
using Xunit;

namespace SharBucketTests.V2.EndPoints
{
    public class UsersEndPointTests
    {
        private SharpBucketV2 sharpBucket;
        private UsersEndpoint usersEndPoint;
        private const string ACCOUNT_NAME = "mirror";

       public UsersEndPointTests()
        {
            sharpBucket = TestHelpers.GetV2ClientAuthenticatedWithOAuth();
            usersEndPoint = sharpBucket.UsersEndPoint(ACCOUNT_NAME);
        }

        [Fact]
        public void GetProfile_FromMirrorAccount_ShouldReturnTheMirrorProfile()
        {
            usersEndPoint.ShouldNotBe(null);
            var profile = usersEndPoint.GetProfile();
            profile.display_name.ShouldBe("mirror");
            profile.created_on.ShouldBe("2008-06-26T13:58:38+00:00");
        }

        [Fact]
        public void ListFollowers_FromMirrorAccount_ShouldReturnMirrorsFollowers()
        {
            usersEndPoint.ShouldNotBe(null);
            var followers = usersEndPoint.ListFollowers(15);
            followers.Count.ShouldBe(15);
            followers[0].display_name.ShouldBe("z19");
        }

        [Fact]
        public void ListFollowing_FromMirrorAccount_ShouldReturnMirrorMembers()
        {
            usersEndPoint.ShouldNotBe(null);
            var following = usersEndPoint.ListFollowing();
            following.Count.ShouldBe(1);
            following[0].display_name.ShouldBe("Jesper Noehr");
        }

        [Fact]
        public void ListRepositories_FromMirrorAccount_ShouldReturnMirrorsRepositories()
        {
            usersEndPoint.ShouldNotBe(null);
            var repositories = usersEndPoint.ListRepositories();
            repositories.Count.ShouldBeGreaterThan(10);
            repositories = usersEndPoint.ListRepositories(max: 25);
            repositories.Count.ShouldBe(25);
        }
    }
}